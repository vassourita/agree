import { GetServerSideProps } from "next"
import Head from "next/head"
import { useRouter } from "next/router"
import { parseCookies } from "nookies"
import { User } from "@logic/models/User"
import { getAccountByIdRequest } from "@logic/services/accord"
import { Header } from "@presentation/components/Header"

export default function UserChat({ user }: { user: User }) {
  return (
    <div>
      <Head>
        <title>Home - Agree</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <Header />

      <hr />

      {user ? (
        <main>
          <h2>Chat with {user.nameTag}</h2>
        </main>
      ) : (
        <main>
          <h2>This user does not exists.</h2>
        </main>
      )}
    </div>
  )
}

export const getServerSideProps: GetServerSideProps = async (ctx) => {
  const { ['agreeaccord_accesstoken']: token } = parseCookies(ctx)

  if (!token) {
    return {
      redirect: {
        destination: '/account/login',
        permanent: false,
      }
    }
  }

  const userId = ctx.query.id as string

  const user = await getAccountByIdRequest(userId, ctx)

  return {
    props: {
      user: user
    }
  }
}