using System;
using System.Diagnostics;
using System.Text;

namespace CLE
{
    class Command
    {
        public static string Exec(string command)
        {
            var output = new StringBuilder();
            var errorOutput = new StringBuilder();

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = $"/c {command}";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;

                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data != null)
                        {
                            output.AppendLine(e.Data);
                        }
                    };

                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data != null)
                        {
                            errorOutput.AppendLine(e.Data);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Command failed with exit code {process.ExitCode}:\n{errorOutput}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while executing the command: {ex.Message}\n{errorOutput}");
            }

            return output.ToString();
        }
    }
}
