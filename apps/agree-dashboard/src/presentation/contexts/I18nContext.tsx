import { createContext, ReactNode, useEffect, useState } from 'react'
import { ILogger } from '../../logic/services/ILogger'

export type Language = 'en-US' | 'pt-BR'

export type Resource = {
  [lang in Language]: {
    [key: string]: string
  }
}

export type I18nContextProps = {
  language: Language
  t(string: TemplateStringsArray, ...values: string[]): string
}

export const I18nContext = createContext<I18nContextProps>({} as I18nContextProps)

type I18nProviderProps = {
  children: ReactNode
  resource: Resource
  logger: ILogger
}

export function I18nProvider ({ children, resource, logger }: I18nProviderProps): JSX.Element {
  const [language, setLanguage] = useState<Language>(navigator.language as Language)

  useEffect(() => {
    if (['en-US', 'pt-BR', 'pt', 'en'].includes(navigator.language)) {
      if (navigator.language === 'en') {
        setLanguage('en-US')
      } else if (navigator.language === 'pt') {
        setLanguage('pt-BR')
      } else {
        setLanguage(navigator.language as Language)
      }
    } else {
      setLanguage('en-US')
    }
  }, [navigator.language])

  function formatTaggedTemplate (replace: boolean, strings: TemplateStringsArray, ...values: unknown[]) {
    let result = ''

    strings.map((str, index) => {
      let n = (index <= values.length - 1) ? values[index] : ''

      if (replace) {
        n = `{${index}}`
      }

      result += str + n
      return str
    })
    return result
  }

  function t (strings: TemplateStringsArray, ...values: string[]): string {
    const currentLanguageResource = resource[language]

    const formattedText = strings.raw.join('')

    const text = currentLanguageResource[formattedText]
    if (text) return text

    return formatTaggedTemplate(false, strings, values)
  }

  return (
    <I18nContext.Provider value={{ language, t }}>
      {children}
    </I18nContext.Provider>
  )
}
