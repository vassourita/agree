import { ILogger, LoggingLevel } from '../ILogger'

export class ConsoleLogger implements ILogger {
  public log (level: LoggingLevel, message: unknown): void {
    console[level](message)
  }

  public info (message: unknown): void {
    console.log(message)
  }

  public warn (message: unknown): void {
    console.warn(message)
  }

  public error (message: unknown): void {
    console.error(message)
  }

  private format (level: string, message: unknown) {
    return `[${level.toUpperCase()} - ${new Date()}] ${message}`
  }
}
