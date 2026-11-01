using Robust.Shared.Timing;
using Robust.Shared.Toolshed;
using Robust.Shared.Toolshed.Syntax;

namespace Content.Trauma.Shared.Administration.Toolshed
{
    [ToolshedCommand]
    public sealed class DelayCommand : ToolshedCommand
    {
        /// <summary>
        /// Syntax: > delay 5.5 { self kill }
        /// </summary>
        [CommandImplementation]
        public void Delay(
            [CommandInvocationContext] IInvocationContext ctx,
            [CommandArgument] float seconds,
            [CommandArgument] Block block)
        {
            var delayTime = TimeSpan.FromSeconds(seconds);

            ctx.WriteLine($"[Toolshed] Deferring execution of command block for {seconds}s...");

            Timer.Spawn(delayTime, () =>
            {
                block.Invoke(null, ctx);
            });
        }

        /// <summary>
        /// Syntax: > entities | delay 2.0 { delete }
        /// </summary>
        [CommandImplementation]
        public void Delay<T>(
            [CommandInvocationContext] IInvocationContext ctx,
            [PipedArgument] T input,
            [CommandArgument] float seconds,
            [CommandArgument] Block<T, object> block)
        {
            var delayTime = TimeSpan.FromSeconds(seconds);

            ctx.WriteLine($"[Toolshed] Deferring execution of command block for {seconds}s...");

            Timer.Spawn(delayTime, () =>
            {
                block.Invoke(input, ctx);
            });
        }
    }
}
