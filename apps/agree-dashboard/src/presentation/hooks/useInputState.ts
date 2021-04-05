import { ChangeEventHandler, KeyboardEvent, useState } from 'react'

export function useInputState (defaultValue?: string): [string, ChangeEventHandler<HTMLInputElement>] {
  const [state, setState] = useState<string>(defaultValue || '')

  const onChange: ChangeEventHandler<HTMLInputElement> = (e) => {
    setState(e.target.value)
  }

  return [state, onChange]
}
