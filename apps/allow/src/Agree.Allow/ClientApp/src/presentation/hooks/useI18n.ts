import { useContext } from 'react'
import { I18nContext, I18nContextProps } from '../contexts/I18nContext'

export function useI18n (): I18nContextProps {
  const ctx = useContext(I18nContext)

  return ctx
}
