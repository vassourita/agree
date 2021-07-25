import '../presentation/styles/globals.css'
import type { AppProps } from 'next/app'
import { AuthContextProvider } from '../logic/contexts/AuthContext'
import { FriendshipContextProvider } from '../logic/contexts/FriendshipContext'

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
