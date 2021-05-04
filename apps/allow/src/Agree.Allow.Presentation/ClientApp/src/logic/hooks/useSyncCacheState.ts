import { useEffect } from 'react'
import { ICache } from '../services/ICache'

export function useSyncCacheState<T> (cache: ICache, key: string, defaultValue?: T): [() => T | null | null, (value: T | null) => void, () => void] {
  useEffect(() => {
    if (!cache.get(key) && !!defaultValue) {
      cache.set(key, defaultValue)
    }
  }, [])

  return [
    () => cache.get<T | null>(key),
    (value: T | null) => cache.set(key, value),
    () => cache.delete(key)
  ]
}
