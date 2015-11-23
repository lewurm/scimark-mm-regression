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
		// exe.StartInfo.FileName = "/Users/bernhardu/work/mono/b/bin/mono";
		// exe.StartInfo.FileName = "/home/lewurm/monoperf/installation/opt/mono-2015.10.29+01.40.50/bin/mono-sgen";
		// exe.StartInfo.WorkingDirectory = "/Users/bernhardu/work/benchmarker/tests/scimark/";
		exe.StartInfo.WorkingDirectory = "/home/lewurm/monoperf/benchmarker/tests/scimark/";

		exe.StartInfo.FileName = "/home/lewurm/monoperf/linux/tools/perf/perf";
		exe.StartInfo.Arguments =  "stat -d  /home/lewurm/monoperf/monobuild/bin/mono-sgen -O=-aot scimark.exe MM";
#if false
		// gdb fun
		exe.StartInfo.FileName = "gdb";
		exe.StartInfo.Arguments =  "--args /home/lewurm/monoperf/monobuild/bin/mono-sgen --stats --break SparseCompRow:matmult -O=-aot scimark.exe MM";
#endif

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

