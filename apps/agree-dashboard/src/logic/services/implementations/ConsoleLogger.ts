import { ILogger, LoggingLevel } from '../ILogger'

export class ConsoleLogger implements ILogger {
  public log (level: LoggingLevel, message: unknown): void {
    if (process.env.NODE_ENV !== 'production') console[level](message)
  }

  public info (message: unknown): void {
    if (process.env.NODE_ENV !== 'production') console.log(message)
  }

  public warn (message: unknown): void {
    if (process.env.NODE_ENV !== 'production') console.warn(message)
  }

  public error (message: unknown): void {
    if (process.env.NODE_ENV !== 'production') console.error(message)
  }
}
