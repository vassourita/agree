import { useBreakpoint } from "@chakra-ui/react"

export function useBreakpoints (): boolean[] {
  const breakpoint = useBreakpoint()

  return [
    breakpoint === 'base',
    ['base', 'sm'].includes(breakpoint || ''),
    ['base', 'sm', 'md'].includes(breakpoint || ''),
    ['base', 'sm', 'md', 'lg'].includes(breakpoint || ''),
    ['base', 'sm', 'md', 'lg', ' xl'].includes(breakpoint || '')
  ]
}