import { ICache } from '../ICache'

export class LocalStorageCache implements ICache {
  get<T> (key: string): T | null {
    const item = localStorage.getItem(key)
    if (!item) {
      return null
    }
    return JSON.parse(item) as T
  }

  set<T> (key: string, value: T): void {
    localStorage.setItem(key, JSON.stringify(value))
  }

  delete (key: string): void {
    localStorage.removeItem(key)
  }
}
