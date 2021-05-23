defmodule AccordWeb.Plugs.Auth do
  import Plug.Conn

  def init(default), do: default

  def call(conn, _options) do
    cookie = conn.req_cookies["agreeallow_accesstoken"]

    if cookie != nil do
      conn
    else
      conn
      |> put_status(:unauthorized)
    end
  end
end
