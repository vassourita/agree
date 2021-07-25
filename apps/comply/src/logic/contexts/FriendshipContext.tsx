import React, { PropsWithChildren, useContext, useEffect, useState } from "react"
import { ErrorList } from "../models/ErrorList"
import { FriendshipRequest } from "../models/FriendshipRequest"
import { User } from "../models/User"
import { accord } from "../services/accord"
import { SignalRService } from "../services/signalr"
import { AuthContext } from "./AuthContext"

type FriendshipContextData = {
  friends: User[],
  sentRequests: FriendshipRequest[],
  receivedRequests: FriendshipRequest[],
  sendFriendRequest(toUser: User): Promise<ErrorList | null>,
  acceptFriendRequest(friend: User): Promise<ErrorList | null>,
  declineFriendRequest(friend: User): Promise<ErrorList | null>,
  searchUsers(q: string): Promise<User[]>,
}

export const FriendshipContext = React.createContext<FriendshipContextData>({} as FriendshipContextData)

export function FriendshipContextProvider(props: PropsWithChildren<any>) {
  const auth = useContext(AuthContext)

  const [friends, setFriends] = useState<User[]>([])
  const [sentRequests, setSentRequests] = useState<FriendshipRequest[]>([])
  const [receivedRequests, setReceivedRequests] = useState<FriendshipRequest[]>([])
  const [friendshipHub, setFriendshipHub] = useState<SignalRService>()

  async function searchUsers(q: string) {
    const response = await accord.get(`/api/identity/accounts?q=${encodeURI(q).replace(/#/g, '%23')}`)
    return (response.data.users as User[]).filter(user => user.id !== auth.user?.id && !friends.map(f => f.id).includes(user.id))
  }

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
      return null
    }
    return response.data.errors as ErrorList
  }

  async function declineFriendRequest(friend: User) {
    const response = await accord.delete(`/api/friendship-requests/${friend.id}`)
    if (response.status === 200) {
      setReceivedRequests(f => f.filter(f => f.from.id !== friend.id))
      return null
    }
    return response.data.errors as ErrorList
  }

  async function sendFriendRequest(toUser: User) {
    const response = await accord.post(`/api/friendship-requests`, { toNameTag: toUser.nameTag })
    if (response.status === 200) {
      setSentRequests(f => [...f, { to: toUser, from: auth.user } as FriendshipRequest])
      return null
    }
    return response.data.errors as ErrorList
  }

  useEffect(() => {
    setFriendshipHub(new SignalRService("/friendships"))
  }, [])

  useEffect(() => {
    fetchFriends()
    fetchSentRequests()
    fetchReceivedRequests()
  }, [auth.user?.id])

  useEffect(() => {
    if (auth.isAuthenticated && auth.isReady && friendshipHub) {
      friendshipHub.startConnection()
      friendshipHub.hubConnection.off("friendship_request_received")
      friendshipHub.hubConnection.off("friendship_request_declined")
      friendshipHub.hubConnection.off("friendship_request_accepted")
      friendshipHub.hubConnection.on("friendship_request_received", (req: FriendshipRequest) => {
        setReceivedRequests(f => [...f, req])
      })
      friendshipHub.hubConnection.on("friendship_request_declined", (req: FriendshipRequest) => {
        console.log(sentRequests)
        console.log(req)
        setSentRequests(f => f.filter(f => f.to.id !== req.to.id))
      })
      friendshipHub.hubConnection.on("friendship_request_accepted", (req: FriendshipRequest) => {
        setSentRequests(f => f.filter(f => f.to.id !== req.to.id))
        addFriend(req.to)
      })
    }
  }, [auth.isAuthenticated, auth.isReady, friendshipHub])

  return (
    <FriendshipContext.Provider value={{ friends, sentRequests, receivedRequests, acceptFriendRequest, declineFriendRequest, sendFriendRequest, searchUsers }}>
      {props.children}
    </FriendshipContext.Provider>
  )
}