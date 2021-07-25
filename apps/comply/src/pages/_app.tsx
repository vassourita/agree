import '../styles/globals.css'
import type { AppProps } from 'next/app'
import { AuthContextProvider } from '../contexts/AuthContext'
import { FriendshipContextProvider } from '../contexts/FriendshipContext'

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <AuthContextProvider>
      <FriendshipContextProvider>
        <Component {...pageProps} />
      </FriendshipContextProvider>
    </AuthContextProvider>
  )
}
export default MyApp
