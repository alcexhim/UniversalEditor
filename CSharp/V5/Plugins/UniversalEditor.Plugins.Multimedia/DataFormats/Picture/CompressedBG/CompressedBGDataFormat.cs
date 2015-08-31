using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture.CompressedBG
{
    public class CompressedBGDataFormat : DataFormat 
    {

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }

        #region Private Implementation

        private struct bg_huffman_node
        {
            public bool valid;				/* ÊÇ·ñÓÐÐ§µÄ±ê¼Ç */
            public uint weight;			    /* È¨Öµ */
            public bool is_parent;			/* ÊÇ·ñÊÇ¸¸½Úµã */
            public uint parent_index;		/* ¸¸½ÚµãË÷Òý */
            public uint left_child_index;	/* ×ó×Ó½ÚµãË÷Òý */
            public uint right_child_index;	/* ÓÒ×Ó½ÚµãË÷Òý */
        }
        private struct bg_header_t
        {
            public uint encode_length;
            public uint key;
            public byte sum_check;
            public byte xor_check;
			public uint zero_comprlen;
			public uint width;
			public uint height;
			public int color_depth;
        }

        private static byte update_key(ref uint key, byte[] magic)
        {
            uint v0, v1;
            v0 = (key & 0xffff) * 20021;
			v1 = ((uint)(magic[1] << 24) | (uint)(magic[0] << 16) | (uint)(key >> 16));
            v1 = v1 * 20021 + key * 346;
            v1 = (v1 + (v0 >> 16)) & 0xffff; 
            key = (uint)((v1 << 16) + (v0 & 0xffff) + 1); 
            return (byte)(v1 & 0x7fff); 
        }

        /********************* CompressedBG___ *********************/ 
        private static void decode_bg(ref byte[] enc_buf, uint enc_buf_len, uint key, ref byte ret_sum, ref byte ret_xor)
        {
            byte sum = 0;
            byte xor = 0;
            byte[] magic = new byte[] { 0, 0 };
            for (uint i = 0; i < enc_buf_len; i++)
            {
	            enc_buf[i] -= update_key(ref key, magic); 
	            sum += enc_buf[i];
	            xor ^= enc_buf[i];
            } 
            ret_sum = sum;
            ret_xor = xor;
        }
        
        private static uint bg_create_huffman_tree(bg_huffman_node[] nodes, uint[] leaf_nodes_weight)
        {
            uint parent_node_index = 256;	/* ¸¸½Úµã´Ónodes[]µÄ256´¦¿ªÊ¼ */ 
            bg_huffman_node parent_node = nodes[parent_node_index];
            uint root_node_weight = 0;	/* ¸ù½ÚµãÈ¨Öµ */ 
            uint i;
 
            /* ³õÊ¼»¯Ò¶½Úµã */ 
            for (i = 0; i < 256; i++)
            {
	            nodes[i].valid = (leaf_nodes_weight[i] != 0);
	            nodes[i].weight = leaf_nodes_weight[i]; 
	            nodes[i].is_parent = false; 
	            root_node_weight += nodes[i].weight; 
            } 
 
            while (true)
            {
	            uint[] child_node_index = new uint[2];
		 
	            /* ´´½¨×óÓÒ×Ó½Úµã */ 
	            for (i = 0; i < 2; i++)
                {
		            uint min_weight;
			 
		            min_weight = unchecked((uint)-1); 
		            child_node_index[i] = unchecked((uint)-1); 
		            /* ±éÀúnodes[], ÕÒµ½weight×îÐ¡µÄ2¸ö½Úµã×÷Îª×Ó½Úµã */ 
		            for (uint n = 0; n < parent_node_index; n++)
                    {
			            if (nodes[n].valid)
                        { 
				            if (nodes[n].weight < min_weight)
                            { 
					            min_weight = nodes[n].weight; 
					            child_node_index[i] = n; 
				            }
			            } 
		            } 
		            /* ±»ÕÒµ½µÄ×Ó½Úµã±ê¼ÇÎªÎÞÐ§£¬ÒÔ±ã²»²ÎÓë½ÓÏÂÀ´µÄ²éÕÒ */			 
		            nodes[child_node_index[i]].valid = false; 
		            nodes[child_node_index[i]].parent_index = parent_node_index; 
	            } 
	            /* ´´½¨¸¸½Úµã */		 
	            parent_node.valid = true; 
	            parent_node.is_parent = true; 
	            parent_node.left_child_index = child_node_index[0]; 
	            parent_node.right_child_index = child_node_index[1]; 
	            parent_node.weight = nodes[parent_node.left_child_index].weight 
		            + nodes[parent_node.right_child_index].weight;	 
	            if (parent_node.weight == root_node_weight) 
                {
		            break;
                }
	            parent_node = nodes[++parent_node_index];
            }
 
            return parent_node_index; 
        }
        private static uint bg_huffman_decompress(bg_huffman_node[] huffman_nodes, uint root_node_index, byte[] uncompr, uint uncomprlen, byte[] compr, uint comprlen)
        {
            // bits bits;
            // bits_init(ref bits, compr, comprlen);
            uint act_uncomprlen = 0;
            for (act_uncomprlen = 0; act_uncomprlen < uncomprlen; act_uncomprlen++)
            {
                byte child = 0;
                uint node_index = root_node_index;
                do
                {
                    if (false) // (bit_get_high(ref bits, ref child))
                    {
                        return act_uncomprlen;
                    }
                    if (child != 0)
                    {
                        node_index = huffman_nodes[node_index].left_child_index; 
                    }
                    else
                    {
                        node_index = huffman_nodes[node_index].right_child_index;
                    }
                }
                while (huffman_nodes[node_index].is_parent);

                uncompr[act_uncomprlen] = (byte)node_index;
            }
            return act_uncomprlen;
        }
        private static uint zero_decompress(byte[] uncompr, uint uncomprlen, byte[] compr, uint comprlen)
        {
            uint act_uncomprlen = 0;
            bool dec_zero = false;
            uint curbyte = 0;
            while (true)
            {
                uint bits = 0;
                uint copy_bytes = 0;
                byte code;
                do
                {
                    if (curbyte >= comprlen) return act_uncomprlen;
                    code = compr[curbyte++];
                    copy_bytes |= (uint)((code & 0x7f) << (int)bits);
                    bits += 7;
                }
                while ((code & 0x80) == 0x80);
                
                if (act_uncomprlen + copy_bytes > uncomprlen) break;
                if (!dec_zero && (curbyte + copy_bytes > comprlen)) break;
                if (!dec_zero)
                {
                    Array.Copy(uncompr, act_uncomprlen, compr, curbyte, copy_bytes);
                    curbyte += copy_bytes;
                    dec_zero = true;
                }
                else
                {
					uncompr.Fill<byte>(0);
                    dec_zero = false; 
                }
                act_uncomprlen += copy_bytes; 
            }
            return act_uncomprlen;
        }
		private static void bg_average_defilting(byte[] dib_buf, uint width, uint height, uint bpp)
        {	
            uint line_len = (width * bpp);
            for (uint y = 0; y < height; y++)
            {
                for (uint x = 0; x < width; x++)
                {
                    for (uint p = 0; p < bpp; p++)
                    {
                        uint a, b;
                        uint avg;
                        b = (y > 0) ? (dib_buf[(y - 1) * line_len + x * bpp + p]) : unchecked((uint)-1);
                        a = (x > 0) ? (dib_buf[y * line_len + (x - 1) * bpp + p]) : unchecked((uint)-1);
                        avg = 0;

				        if (a != unchecked((uint)-1)) avg += a;
						if (b != unchecked((uint)-1)) avg += b;
						if (a != unchecked((uint)-1) && b != unchecked((uint)-1)) avg /= 2; 
                        
                        dib_buf[y * line_len + x * bpp + p] = (byte)(dib_buf[y * line_len + x * bpp + p] + avg);
                    }
                }
            }
        }
        private static uint bg_decompress(ref bg_header_t bg_header, ref byte[] enc_buf, uint bg_len, byte[] image_buf, uint image_size)
        {
	        uint act_uncomprlen = 0;
	        uint i;

            /* ½âÃÜÒ¶½ÚµãÈ¨Öµ */ 
            byte sum = 0;
            byte xor = 0;
            decode_bg(ref enc_buf, bg_header.encode_length, bg_header.key, ref sum, ref xor);

            if (sum != bg_header.sum_check || xor != bg_header.xor_check)
            {
                throw new InvalidOperationException("checksum mismatch");
            }

            /* ³õÊ¼»¯Ò¶½ÚµãÈ¨Öµ */
            uint[] leaf_nodes_weight = new uint[256];
            uint curbyte = 0;
            for (i = 0; i < 256; i++)
            {
                uint bits = 0;
                uint weight = 0;
                byte code;
                do
                {
                    if (curbyte >= bg_header.encode_length) return 0;
                    code = enc_buf[curbyte++];
                    weight |= (uint)((code & 0x7f) << (int)bits);
                    bits += 7;
                }
                while ((code & 0x80) == 0x80);
                leaf_nodes_weight[i] = weight;
            }

            bg_huffman_node[] nodes = new bg_huffman_node[511];
            uint root_node_index = bg_create_huffman_tree(nodes, leaf_nodes_weight);
            byte[] zero_compr = new byte[bg_header.zero_comprlen]; // will throw OutOfMemoryException if doesn't work

            byte[] compr = new byte[enc_buf.Length - bg_header.encode_length];
			Array.Copy(enc_buf, bg_header.encode_length, compr, 0, compr.Length);

            uint comprlen = (uint)(bg_len - System.Runtime.InteropServices.Marshal.SizeOf(bg_header) - bg_header.encode_length);
            act_uncomprlen = bg_huffman_decompress(nodes, root_node_index, zero_compr, bg_header.zero_comprlen, compr, comprlen);
            if (act_uncomprlen != bg_header.zero_comprlen)
            {
                zero_compr = null;
                throw new Compression.CompressionFailureException("uncompressed length mismatch", Compression.CompressionMethod.Unknown, Compression.CompressionMode.Decompress);
            }
            
            act_uncomprlen = zero_decompress(image_buf, image_size, zero_compr, bg_header.zero_comprlen);
            zero_compr = null;
            
            bg_average_defilting(image_buf, bg_header.width, bg_header.height, (uint)(bg_header.color_depth / 8));
            return act_uncomprlen;
        }
        #endregion
    }
}