import { GetServerSideProps } from "next";
import Head from "next/head";
import Link from "next/link";
import Router from "next/router";
import { parseCookies } from "nookies";
import { FormEvent, useContext, useState } from "react";
import { FriendshipContext } from "@logic/contexts/FriendshipContext";
import { ErrorList } from "@logic/models/ErrorList";
import { User } from "@logic/models/User";
import { ErrorAlert } from "@presentation/components/ErrorAlert";
import { Header } from "@presentation/components/Header";

export default function NewFriendshipRequest() {
  const friendship = useContext(FriendshipContext)

  const [nameTag, setNameTag] = useState('')
  const [errors, setErrors] = useState<ErrorList>()
  const [users, setUsers] = useState<User[]>()

  async function handleSubmit(user: User) {
    var errors = await friendship.sendFriendRequest(user)
    if (errors) {
      setErrors(errors)
    } else {
      Router.push('/friends')
    }
  }

  async function handleInputChange(value: string) {
    setNameTag(value)
    if (!value) {
      setUsers([])
      return
    }
    var users = await friendship.searchUsers(value)
    setUsers(users)
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

        {errors && (
          <>
            <ErrorAlert errors={errors} />
            <br />
          </>
        )}

        <div>
          <div>
            <label htmlFor="nametag">NameTag</label>
            <br />
            <input autoComplete="off" type="nametag" id="nametag" value={nameTag} onChange={e => handleInputChange(e.target.value)} />
          </div>
          <ul>
            {users && users.map(user => (
              <li key={user.id}>
                {user.nameTag}
                <button onClick={() => handleSubmit(user)}>Send</button>
              </li>
            ))}
          </ul>
          <br />
          <button>Send</button>
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