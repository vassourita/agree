defmodule AccordWeb.AccountController do
  use AccordWeb, :controller

  def me(conn, _params) do
    conn
    |> json(%{id: 1})
  end
end
