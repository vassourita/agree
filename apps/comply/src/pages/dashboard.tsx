import { GetServerSideProps } from "next";
import Head from "next/head";
import Link from "next/link";
import { parseCookies } from "nookies";
import { useContext, useState } from "react";
import { FriendshipContext } from "../logic/contexts/FriendshipContext";
import { ErrorList } from "../logic/models/ErrorList";
import { FriendshipRequest } from "../logic/models/FriendshipRequest";
import { Header } from "../presentation/components/Header";

export default function Dashboard() {
  const friendship = useContext(FriendshipContext)

  const [errors, setErrors] = useState<ErrorList>()

  async function accept(req: FriendshipRequest) {
    var errors = await friendship.acceptFriendRequest(req.from)
    if (errors) {
      setErrors(errors)
    }
  }

  async function decline(req: FriendshipRequest) {
    var errors = await friendship.declineFriendRequest(req.from)
    if (errors) {
      setErrors(errors)
    }
  }

  return (
    <div>
      <Head>
        <title>Home - Agree</title>
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <Header />

      <hr />

      <main>
        <h2>Dashboard</h2>

        <div>
          <Link passHref href="/friends">
            <h5>Friends</h5>
          </Link>
        </div>

        <div>
          <Link passHref href="/servers">
            <h5>Servers</h5>
          </Link>
        </div>
      </main>
    </div>
  );
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

  return {
    props: {}
  }
}