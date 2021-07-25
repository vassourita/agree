import React, { PropsWithChildren, useContext, useEffect, useState } from "react"
import { FriendshipRequest } from "../models/FriendshipRequest"
import { User } from "../models/User"
import { accord } from "../services/accord"
import { AuthContext } from "./AuthContext"

type FriendshipContextData = {
  friends: User[],
  sentRequests: FriendshipRequest[],
  receivedRequests: FriendshipRequest[],
  sendFriendRequest(toUser: User): Promise<boolean>,
  acceptFriendRequest(friend: User): Promise<boolean>,
  declineFriendRequest(friend: User): Promise<boolean>
}

export const FriendshipContext = React.createContext<FriendshipContextData>({} as FriendshipContextData)

export function FriendshipContextProvider(props: PropsWithChildren<any>) {
  const auth = useContext(AuthContext)

  const [friends, setFriends] = useState<User[]>([])
  const [sentRequests, setSentRequests] = useState<FriendshipRequest[]>([])
  const [receivedRequests, setReceivedRequests] = useState<FriendshipRequest[]>([])

  async function fetchFriends() {
    const response = await accord.get(`/api/friends`)
    if (response.status === 200)
      setFriends(response.data.friends)
  }

  async function fetchSentRequests() {
    const response = await accord.get(`/api/friendship-requests/sent`)
    if (response.status === 200)
      setSentRequests(response.data.requests)
  }

  async function fetchReceivedRequests() {
    const response = await accord.get(`/api/friendship-requests/received`)
    if (response.status === 200)
      setReceivedRequests(response.data.requests)
  }

  function addFriend(friend: User) {
    setFriends(f => [...f, friend])
  }

  async function acceptFriendRequest(friend: User) {
    const response = await accord.put(`/api/friendship-requests/${friend.id}`)
    if (response.status === 200) {
      setReceivedRequests(f => f.filter(f => f.from.id !== friend.id))
      addFriend(friend)
      return true
    }
    return false
  }

  async function declineFriendRequest(friend: User) {
    const response = await accord.delete(`/api/friendship-requests/${friend.id}`)
    if (response.status === 200) {
      setReceivedRequests(f => f.filter(f => f.from.id !== friend.id))
      return true
    }
    return false
  }

  async function sendFriendRequest(toUser: User) {
    const response = await accord.post(`/api/friendship-requests`, { toNameTag: toUser.nameTag })
    if (response.status === 200) {
      setSentRequests(f => [...f, { to: toUser, from: auth.user } as FriendshipRequest])
      return true
    }
    return false
  }

  useEffect(() => {
    fetchFriends()
    fetchSentRequests()
    fetchReceivedRequests()
  }, [auth.user?.id])

  return (
    <FriendshipContext.Provider value={{ friends, sentRequests, receivedRequests, acceptFriendRequest, declineFriendRequest, sendFriendRequest }}>
      {props.children}
    </FriendshipContext.Provider>
  )
}