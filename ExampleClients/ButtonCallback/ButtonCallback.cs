﻿/// Managed-OSVR binding
///
/// <copyright>
/// Copyright 2014, 2015 Sensics, Inc. and contributors
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///     http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </copyright>

using System;
using OSVR.ClientKit;

namespace ButtonCallback
{
    class ButtonCallback
    {
        static void button1_StateChanged(object sender, TimeValue timestamp, Int32 sensor, Byte report)
        {
            Console.WriteLine("Got report: button from sensor {0} is {1}", sensor, report == ButtonInterface.Pressed ? "pressed" : "released");
        }
        static void Main(string[] args)
        {
            ClientContext.PreloadNativeLibraries();
			using (OSVR.ClientKit.ClientContext context = new OSVR.ClientKit.ClientContext("com.osvr.exampleclients.managed.ButtonCallback"))
            {
                // This is just one of the paths: specifically, the Hydra's left
                // controller's button labelled "1". More are in the docs and/or listed on
                // startup
#if NET20
                using (var button1 = ButtonInterface.GetInterface(context, "/controller/left/1"))
#else
                using (var button1 = context.GetButtonInterface("/controller/left/1"))
#endif
                {
                    button1.StateChanged += button1_StateChanged;
                    // Pretend that this is your application's main loop
                    for (int i = 0; i < 1000000; ++i)
                    {
                        context.update();
                    }

                    Console.WriteLine("Library shut down; exiting.");
                }
            }
        }
    }
}
