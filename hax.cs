using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

public class Hax {
	public static void Main() {
		Process exe = new Process ();
		exe.StartInfo.EnvironmentVariables.Clear ();
		exe.StartInfo.UseShellExecute = false;
		exe.StartInfo.RedirectStandardOutput = true;
		exe.StartInfo.RedirectStandardError = true;
#if FAST
		exe.StartInfo.EnvironmentVariables["LANG"] = "C.UTF-8";
#endif
		exe.StartInfo.FileName = "./perf";
		exe.StartInfo.Arguments = "stat -d ./monobuild/bin/mono-sgen -O=-aot scimark.exe MM";

		exe.OutputDataReceived += (sender, args) => Console.WriteLine ("{0}", args.Data);
		exe.ErrorDataReceived += (sender, args) => Console.WriteLine ("{0}", args.Data);
		exe.Start ();
		exe.BeginOutputReadLine ();
		exe.BeginErrorReadLine ();

		exe.WaitForExit ();
		if (exe.ExitCode != 0) {
			Console.Out.WriteLine ("forking failed a bit");
		}
	}
}

