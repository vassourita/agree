defmodule AccordWeb.AccountController do
  use AccordWeb, :controller

  def me(conn, _params) do
    conn
    |> json(%{user: conn.assigns[:user]})
  end
end
