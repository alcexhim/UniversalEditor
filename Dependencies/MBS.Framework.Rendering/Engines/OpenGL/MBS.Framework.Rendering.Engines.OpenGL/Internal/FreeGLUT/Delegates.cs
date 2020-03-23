using System;
namespace MBS.Framework.Rendering.Engines.OpenGL.Internal.FreeGLUT
{
    public class Delegates
    {
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutButtonBoxFunc(Tao.FreeGlut.Glut.ButtonBoxCallback).
        /// </summary>
        /// <param name="button"></param>
        /// <param name="state"></param>
        public delegate void ButtonBoxCallback(int button, int state);
        /// <summary>
        /// Callback (delegate for use with Tao.FreeGlut.Glut.glutCloseFunc(Tao.FreeGlut.Glut.CloseCallback).
        /// </summary>
        public delegate void CloseCallback();
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutCreateMenu(Tao.FreeGlut.Glut.CreateMenuCallback).
        /// </summary>
        /// <param name="val"></param>
        public delegate void CreateMenuCallback(int val);
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutDialsFunc(Tao.FreeGlut.Glut.DialsCallback).
        /// </summary>
        /// <param name="dial"></param>
        /// <param name="val"></param>
        public delegate void DialsCallback(int dial, int val);
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutDisplayFunc(Tao.FreeGlut.Glut.DisplayCallback).
        /// </summary>
        public delegate void DisplayCallback();

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutEntryFunc(Tao.FreeGlut.Glut.EntryCallback).
        public delegate void EntryCallback(int state);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutIdleFunc(Tao.FreeGlut.Glut.IdleCallback).
        public delegate void IdleCallback();

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutJoystickFunc(Tao.FreeGlut.Glut.JoystickCallback,System.Int32).
        public delegate void JoystickCallback(int buttonMask, int x, int y, int z);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutKeyboardFunc(Tao.FreeGlut.Glut.KeyboardCallback).
        public delegate void KeyboardCallback(byte key, int x, int y);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutKeyboardUpFunc(Tao.FreeGlut.Glut.KeyboardUpCallback).
        public delegate void KeyboardUpCallback(byte key, int x, int y);

        // Summary:
        //     Callback (delegate for use with Tao.FreeGlut.Glut.glutMenuDestroyFunc(Tao.FreeGlut.Glut.MenuDestroyCallback).
        public delegate void MenuDestroyCallback();

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutMenuStateFunc(Tao.FreeGlut.Glut.MenuStateCallback).
        public delegate void MenuStateCallback(int state);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutMenuStatusFunc(Tao.FreeGlut.Glut.MenuStatusCallback).
        public delegate void MenuStatusCallback(int status, int x, int y);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutMotionFunc(Tao.FreeGlut.Glut.MotionCallback).
        public delegate void MotionCallback(int x, int y);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutMouseFunc(Tao.FreeGlut.Glut.MouseCallback).
        public delegate void MouseCallback(int button, int state, int x, int y);

        // Summary:
        //     Callback (delegate for use with Tao.FreeGlut.Glut.glutMouseWheelFunc(Tao.FreeGlut.Glut.MouseWheelCallback).
        //
        // Parameters:
        //   wheel:
        //     Wheel number.
        //
        //   direction:
        //     Direction, +/- 1.
        //
        //   x:
        //     Pointer X coordinate.
        //
        //   y:
        //     Pointer Y coordinate.
        //
        // Remarks:
        //     This may not work reliably on X Windows.
        public delegate void MouseWheelCallback(int wheel, int direction, int x, int y);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutOverlayDisplayFunc(Tao.FreeGlut.Glut.OverlayDisplayCallback).
        public delegate void OverlayDisplayCallback();

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutPassiveMotionFunc(Tao.FreeGlut.Glut.PassiveMotionCallback).
        public delegate void PassiveMotionCallback(int x, int y);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutReshapeFunc(Tao.FreeGlut.Glut.ReshapeCallback).
        public delegate void ReshapeCallback(int width, int height);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutSpaceballButtonFunc(Tao.FreeGlut.Glut.SpaceballButtonCallback).
        public delegate void SpaceballButtonCallback(int button, int state);

        // Summary:
        //     Callback (delegate) for use with Tao.FreeGlut.Glut.glutSpaceballMotionFunc(Tao.FreeGlut.Glut.SpaceballMotionCallback).
        public delegate void SpaceballMotionCallback(int x, int y, int z);
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutSpaceballRotateFunc(Tao.FreeGlut.Glut.SpaceballRotateCallback).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public delegate void SpaceballRotateCallback(int x, int y, int z);
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutSpecialFunc(Tao.FreeGlut.Glut.SpecialCallback).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public delegate void SpecialCallback(int key, int x, int y);
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutSpecialUpFunc(Tao.FreeGlut.Glut.SpecialUpCallback).
        /// </summary>
        /// <param name="key"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public delegate void SpecialUpCallback(int key, int x, int y);
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutTabletButtonFunc(Tao.FreeGlut.Glut.TabletButtonCallback).
        /// </summary>
        /// <param name="button"></param>
        /// <param name="state"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public delegate void TabletButtonCallback(int button, int state, int x, int y);
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutTabletMotionFunc(Tao.FreeGlut.Glut.TabletMotionCallback).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public delegate void TabletMotionCallback(int x, int y);
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutTimerFunc(System.Int32,Tao.FreeGlut.Glut.TimerCallback,System.Int32).
        /// </summary>
        /// <param name="val"></param>
        public delegate void TimerCallback(int val);
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutVisibilityFunc(Tao.FreeGlut.Glut.VisibilityCallback).
        /// </summary>
        /// <param name="state"></param>
        public delegate void VisibilityCallback(int state);
        /// <summary>
        /// Callback (delegate for use with Tao.FreeGlut.Glut.glutWMCloseFunc(Tao.FreeGlut.Glut.WindowCloseCallback).
        /// </summary>
        public delegate void WindowCloseCallback();
        /// <summary>
        /// Callback (delegate) for use with Tao.FreeGlut.Glut.glutWindowStatusFunc(Tao.FreeGlut.Glut.WindowStatusCallback).
        /// </summary>
        /// <param name="state"></param>
        public delegate void WindowStatusCallback(int state);
    }
}

