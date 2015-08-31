using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Icarus
{
    /// <summary>
    /// &lt;CAMERA_COMMANDS&gt;
    /// </summary>
    public enum IcarusCameraOperation
    {
        None = 0,
        /// <summary>
        /// Pan to absolute angle from current angle in dir (no dir will use shortest) over number of milliseconds
        /// </summary>
        Pan = 57,
        /// <summary>
        /// Normal is 80, 10 is zoomed in, max is 120. Second value is time in ms to take to zoom to the new FOV
        /// </summary>
        Zoom = 58,
        /// <summary>
        /// Move to a absolute vector origin or TAG("targetname", ORIGIN) over time over number of milliseconds
        /// </summary>
        Move = 59,
        /// <summary>
        /// Fade from [start Red Green Blue], [Opacity] to [end Red Green Blue], [Opacity] [all fields valid ranges are 0 to 1] over [number of milliseconds]
        /// </summary>
        Fade = 60,
        /// <summary>
        /// Path to ROF file
        /// </summary>
        Path = 61,
        /// <summary>
        /// Puts game in camera mode
        /// </summary>
        Enable = 62,
        /// <summary>
        /// Takes game out of camera mode
        /// </summary>
        Disable = 63,
        /// <summary>
        /// Intensity (0-16) and duration, in milliseconds
        /// </summary>
        Shake = 64,
        /// <summary>
        /// Roll to relative angle offsets of current angle over number of milliseconds
        /// </summary>
        Roll = 65,
        /// <summary>
        /// Get on track and move at speed, last number is whether or not to lerp to the start pos
        /// </summary>
        Track = 66,
        /// <summary>
        /// Keep this distance from cameraGroup (if any), last number is whether or not to lerp to the start angle
        /// </summary>
        Distance = 67,
        /// <summary>
        /// Follow ends with matching cameraGroup at angleSpeed, last number is whether or not to lerp to the start angle
        /// </summary>
        Follow = 68
    }
}
