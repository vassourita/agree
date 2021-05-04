import { useEffect, useRef, useState } from 'react'

type LoadingHook = {
  isLoading: boolean
  loadingMessage?: string
  loadingTime: number
  initLoading(message: string): void
  stopLoading(): void
}

export function useLoading (): LoadingHook {
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [loadingMessage, setLoadingMessage] = useState<string>()
  const [loadingTime, setLoadingTime] = useState<number>(0)

  const loadingTimeout = useRef(null)

  function initLoading (message: string): void {
    setIsLoading(true)
    setLoadingMessage(message)
  }

  function stopLoading (): void {
    setIsLoading(false)
    setLoadingMessage(undefined)
  }

  useEffect(() => {
    if (isLoading) {
      if (loadingTime !== 0) {
        loadingTimeout.current = setTimeout(() => {
          setLoadingTime(current => current + 1)
        }, 1000) as any
      }
    } else {
      clearTimeout(loadingTimeout.current as any)
    }
  }, [isLoading])

  return {
    isLoading,
    loadingMessage,
    loadingTime,
    initLoading,
    stopLoading
  }
}
