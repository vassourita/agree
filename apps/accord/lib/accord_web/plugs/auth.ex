defmodule AccordWeb.Plugs.Auth do
  import Plug.Conn
  import Phoenix.Controller

  alias Accord.AllowIntegration.User, as: Allow

  def init(default), do: default

  def call(conn, _options) do
    token_cookie = conn.req_cookies["agreeallow_accesstoken"]

    if token_cookie == nil do
      conn
      |> put_status(:unauthorized)
      |> json(%{status: 401, message: "Access token is not present in the request"})
      |> halt()
    end

    case Allow.authenticate_from_token(token_cookie) do
      {:ok, user} ->
        conn
        |> assign(:user, user)
        |> assign(:access_token, token_cookie)

      {:error, reason} ->
        conn
        |> put_status(:unauthorized)
        |> json(%{status: 401, message: reason})
        |> halt()
    end
  end
end
