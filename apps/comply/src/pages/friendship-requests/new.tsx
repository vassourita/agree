import { GetServerSideProps } from "next";
import Head from "next/head";
import Link from "next/link";
import Router from "next/router";
import { parseCookies } from "nookies";
import { FormEvent, useContext, useState } from "react";
import { FriendshipContext } from "../../logic/contexts/FriendshipContext";
import { User } from "../../logic/models/User";
import { Header } from "../../presentation/components/Header";

export default function NewFriendshipRequest() {
  const friendship = useContext(FriendshipContext)

  const [nameTag, setNameTag] = useState('')

  async function handleSubmit(e: FormEvent) {
    e.preventDefault()
    const user = {
      nameTag
    }
    var ok = await friendship.sendFriendRequest(user as User)
    if (ok) {
      Router.push('/dashboard')
    } else {
      alert('Error')
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
        <h2>New friendship request</h2>

        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="nametag">NameTag</label>
            <br />
            <input type="nametag" id="nametag" value={nameTag} onChange={e => setNameTag(e.target.value)} />
          </div>
          <br />
          <button>Send</button>
        </form>
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