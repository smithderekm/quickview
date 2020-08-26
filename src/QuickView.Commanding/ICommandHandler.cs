namespace QuickView.Commanding
{
    using System.Threading;
    using System.Threading.Tasks;


    /// <summary>
    /// The CommandHandler interface.
    /// </summary>
    /// <typeparam name="TCommand">
    /// The command type
    /// </typeparam>
    /// <typeparam name="TCommandResult">
    /// The command result
    /// </typeparam>
    public interface ICommandHandler<in TCommand, TCommandResult>
        where TCommand : ICommand
        where TCommandResult : ICommandResult
    {
        /// <summary>
        /// The handle async.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<TCommandResult> HandleAsync(
            TCommand command,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
