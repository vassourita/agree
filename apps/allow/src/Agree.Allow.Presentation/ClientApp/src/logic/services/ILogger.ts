export type LoggingLevel = 'info' | 'warn' | 'error'

export interface ILogger {
  log(level: LoggingLevel, message: any): void
  info(message: any): void
  warn(message: any): void
  error(message: any): void
}
