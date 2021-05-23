defmodule AccordWeb.Plugs.Auth do
  import Plug.Conn
  import Phoenix.Controller

  def init(default), do: default

  def call(conn, _options) do
    cookie = conn.req_cookies["agreeallow_accesstoken"]

    if cookie != nil do
      conn
    else
      conn
      |> put_status(:unauthorized)
      |> json(%{status: 401})
      |> halt()
    end
  end
end
