export interface ICache {
  get (key: string): string | null
  get<T> (key: string): T | null
  set (key: string, value: string): void
  set<T> (key: string, value: T): void
  delete (key: string): void
}
