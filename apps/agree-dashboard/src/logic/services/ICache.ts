export interface ICache {
  get<T> (key: string): T | null
  set<T>(key: string, value: T): void
  delete(key: string): void
}
