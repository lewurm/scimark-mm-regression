using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

public class Hax {
	public static void Main() {
		var info = new ProcessStartInfo {
			UseShellExecute = false,
			RedirectStandardOutput = true,
			RedirectStandardError = true,
		};
		info.EnvironmentVariables.Clear ();
#if FAST
		info.EnvironmentVariables["LANG"] = "C.UTF-8";
#endif
		// info.FileName = "/home/lewurm/monoperf/installation/opt/mono-2015.10.29+01.40.50/bin/mono-sgen";
		// info.WorkingDirectory = "/home/lewurm/work/benchmarker/tests/scimark/";
		info.FileName = "/Users/bernhardu/work/mono/b/bin/mono";
		info.WorkingDirectory = "/Users/bernhardu/work/benchmarker/tests/scimark/";
		info.Arguments = "--stats -O=-aot scimark.exe MM";
		using (Process exe = Process.Start (info)) {
			exe.WaitForExit ();
			var stdout = Task.Factory.StartNew (() => new StreamReader (exe.StandardOutput.BaseStream).ReadToEnd (), TaskCreationOptions.LongRunning);
			var stderr = Task.Factory.StartNew (() => new StreamReader (exe.StandardError.BaseStream).ReadToEnd (), TaskCreationOptions.LongRunning);

			if (exe.ExitCode != 0) {
				Console.Out.WriteLine ("forking failed a bit");
			}

			Console.Out.WriteLine ("stdout:\n{0}", stdout.Result);
			Console.Out.WriteLine ("stderr:\n{0}", stderr.Result);
		}
	}
}

