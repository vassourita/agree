import type { AppProps } from 'next/app'
import { AuthContextProvider } from '@logic/contexts/AuthContext'
import { FriendshipContextProvider } from '@logic/contexts/FriendshipContext'
import { ThemeProvider, CSSReset } from '@chakra-ui/react';
import { chakraTheme } from '@presentation/chakraTheme';
import { Global } from '@emotion/react';

export default function MyApp({ Component, pageProps }: AppProps) {
  return (
    <ThemeProvider theme={chakraTheme}>
      <CSSReset />
      <Global styles={`
        * {
          font-family: 'Sarabun', sans-serif;
        }
      `} />
      <AuthContextProvider>
        <FriendshipContextProvider>
          <Component {...pageProps} />
        </FriendshipContextProvider>
      </AuthContextProvider>
    </ThemeProvider>
  )
}
