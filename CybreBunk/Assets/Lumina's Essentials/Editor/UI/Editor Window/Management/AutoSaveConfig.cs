using UnityEditor;
using UnityEngine;

namespace Lumina.Essentials.Editor.UI.Management
{
    public static class AutoSaveConfig
    {
        /// <summary>
        /// Enable auto save functionality
        /// </summary>
        internal static bool Enabled
        {
            get => EditorPrefs.GetBool("AutoSaveEnabled", false);
            set => EditorPrefs.SetBool("AutoSaveEnabled", value);
        }

        /// <summary>
        /// The frequency in minutes auto save will activate
        /// </summary>
        internal static int Interval
        {
            get => EditorPrefs.GetInt("AutoSaveInterval", 15);
            set => EditorPrefs.SetInt("AutoSaveInterval", value);
        }

        /// <summary>
        /// Log a message every time the scene is auto saved
        /// </summary>
        internal static bool Logging = true;
    }
}
