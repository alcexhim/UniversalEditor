using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Web.WebService.Description;

namespace UniversalEditor.DataFormats.Web.WebService.Description.WSDL
{
    public class WSDLDataFormat : XMLDataFormat
    {
        private DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = new DataFormatReference(GetType());
                _dfr.Capabilities.Add(typeof(WebServiceDescriptionObjectModel), DataFormatCapabilities.All);
                _dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
                _dfr.Filters.Add("Web Services Description Language", new string[] { "*.wsdl" });
            }
            return _dfr;
        }

        protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeLoadInternal(objectModels);
            objectModels.Push(new MarkupObjectModel());
        }
        protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.AfterLoadInternal(objectModels);
            
            MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
            WebServiceDescriptionObjectModel wsdl = (objectModels.Pop() as WebServiceDescriptionObjectModel);
            if (wsdl == null) throw new ObjectModelNotSupportedException();

            MarkupTagElement tagDefinitions = (mom.Elements["wsdl:definitions"] as MarkupTagElement);
            if (tagDefinitions == null) throw new InvalidDataFormatException("File does not contain a \"wsdl:definitions\" tag");

            MarkupAttribute attName = tagDefinitions.Attributes["name"];
            if (attName != null) wsdl.Name = attName.Value;

            MarkupTagElement tagDocumentation = (tagDefinitions.Elements["wsdl:documentation"] as MarkupTagElement);
            if (tagDocumentation != null) wsdl.Description = tagDocumentation.Value;

            foreach (MarkupElement el in tagDefinitions.Elements)
            {
                MarkupTagElement tag = (el as MarkupTagElement);
                if (tag == null) continue;

                switch (tag.FullName)
                {
                    #region Message
                    case "wsdl:message":
                    {
                        MarkupAttribute attMessageName = tag.Attributes["name"];
                        if (attMessageName == null) continue;

                        Message message = new Message();
                        message.Name = attMessageName.Value;

                        foreach (MarkupElement el1 in tag.Elements)
                        {
                            MarkupTagElement tag1 = (el1 as MarkupTagElement);
                            if (tag1 == null) continue;

                            switch (tag1.FullName)
                            {
                                case "wsdl:part":
                                {
                                    MarkupAttribute attPartName = tag1.Attributes["name"];
                                    if (attPartName == null) continue;

                                    MessagePart part = new MessagePart();
                                    part.Name = attPartName.Value;

                                    MarkupAttribute attElement = tag1.Attributes["element"];
                                    if (attElement != null)
                                    {
                                        part.Element = attElement.Value;
                                    }

                                    message.Parts.Add(part);
                                    break;
                                }
                            }
                        }

                        wsdl.Messages.Add(message);
                        break;
                    }
                    #endregion
                    #region Port
                    case "Port":
                    {
                        MarkupAttribute attPortName = tag.Attributes["name"];
                        if (attPortName == null) continue;

                        Port port = new Port();
                        port.Name = attPortName.Value;

                        MarkupTagElement tagPortDocumentation = (tag.Elements["wsdl:documentation"] as MarkupTagElement);
                        if (tagPortDocumentation != null)
                        {
                            port.Description = tagPortDocumentation.Value;
                        }

                        foreach (MarkupElement el1 in tag.Elements)
                        {
                            MarkupTagElement tag1 = (el1 as MarkupTagElement);
                            if (tag1 == null) continue;

                            switch (tag1.FullName)
                            {
                                case "wsdl:operation":
                                {
                                    MarkupAttribute attOperationName = tag1.Attributes["name"];
                                    if (attOperationName == null) continue;

                                    Operation operation = new Operation();
                                    operation.Name = attOperationName.Value;

                                    MarkupTagElement tagOperationDocumentation = (tag1.Elements["wsdl:documentation"] as MarkupTagElement);
                                    if (tagOperationDocumentation != null)
                                    {
                                        operation.Description = tagOperationDocumentation.Value;
                                    }

                                    foreach (MarkupElement el2 in tag1.Elements)
                                    {
                                        MarkupTagElement tag2 = (el2 as MarkupTagElement);
                                        if (tag2 == null) continue;
                                        switch (tag2.FullName)
                                        {
                                            case "wsdl:input":
                                            {
                                                MarkupAttribute attFaultName = tag2.Attributes["name"];
                                                if (attFaultName == null) continue;

                                                MarkupAttribute attMessage = tag2.Attributes["message"];
                                                if (attMessage == null) continue;

                                                Input input = new Input();
                                                input.Name = attFaultName.Value;
                                                input.Message = wsdl.Messages[attMessage.Value];
                                                operation.Inputs.Add(input);
                                                break;
                                            }
                                            case "wsdl:output":
                                            {
                                                MarkupAttribute attFaultName = tag2.Attributes["name"];
                                                if (attFaultName == null) continue;

                                                MarkupAttribute attMessage = tag2.Attributes["message"];
                                                if (attMessage == null) continue;

                                                Output output = new Output();
                                                output.Name = attFaultName.Value;
                                                output.Message = wsdl.Messages[attMessage.Value];
                                                operation.Outputs.Add(output);
                                                break;
                                            }
                                            case "wsdl:fault":
                                            {
                                                MarkupAttribute attFaultName = tag2.Attributes["name"];
                                                if (attFaultName == null) continue;

                                                MarkupAttribute attMessage = tag2.Attributes["message"];
                                                if (attMessage == null) continue;

                                                Fault fault = new Fault();
                                                fault.Name = attFaultName.Value;
                                                fault.Message = wsdl.Messages[attMessage.Value];
                                                operation.Faults.Add(fault);
                                                break;
                                            }
                                        }
                                    }

                                    port.Operations.Add(operation);
                                    break;
                                }
                            }
                        }

                        wsdl.Ports.Add(port);
                        break;
                    }
                    #endregion
                }
            }
        }
        protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            base.BeforeSaveInternal(objectModels);
            WebServiceDescriptionObjectModel wsdl = (objectModels.Pop() as WebServiceDescriptionObjectModel);
            MarkupObjectModel mom = new MarkupObjectModel();

            objectModels.Push(mom);
        }
    }
}
