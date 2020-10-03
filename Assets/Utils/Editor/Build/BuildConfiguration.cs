using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Belwyn.Editor.Build {


    // Scriptable object for a definition of scripting define symbols for compilation of multiple platforms
    // Create a new BuildConfiguration asset by right-clicking in the project window

    [CreateAssetMenu(fileName = "NewConfiguration", menuName = "Build/Configuration")]
    public class BuildConfiguration : ScriptableObject {


        // Struct for the definition of the configuration for a single platform
        // The Define Symbols list is the list of symbols you want to apply to your Scripting Define Symbols
        // The bools for testing and shipping builds are used to add those core symbols in addition to the ones you wrote.
        // This is used for some other utilities of the project that use those symbols
        // Because the directives are processed at compilation time they have to be written directly in the code
        // So just mark these values and you don't have to worry about anything!
        [Serializable]
        public struct BuildConfig {
            [SerializeField]
            private List<string> _defineSymbols;
            public List<string> defineSymbols => _defineSymbols;

            [SerializeField]
            private bool _isTestingBuild;
            public bool isTestingBuild => _isTestingBuild;

            [SerializeField]
            private bool _isShippingBuild;
            public bool isShippingBuild => _isShippingBuild;
        }


        // Base standalone configuration
        [Header("Standalone config")]
        [SerializeField]
        private BuildConfig _standaloneConfig;
        public BuildConfig standaloneConfig => _standaloneConfig;

        [Space()]


        // These are for overriding a diferent configuration for specific platfomrs
        // I only added Android and iOS, add any other you want!

        [Header("Overrides")]

        // Android
        [SerializeField]
        private bool _overrideAndroid;
        public bool overrideAndroid => _overrideAndroid;

        [SerializeField]
        private BuildConfig _androidConfig;
        public BuildConfig androidConfig => _androidConfig;

        [Space()]

        // iOS
        [SerializeField]
        private bool _overrideIOS;
        public bool overrideIOS => _overrideIOS;

        [SerializeField]
        private BuildConfig _iOSConfig;
        public BuildConfig iosConfig => _iOSConfig;


        //
        // Add more platforms here!
        //

    }

}