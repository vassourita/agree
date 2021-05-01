defmodule AccordWeb.UsersController do
  use AccordWeb, :controller
  alias Accord.Account.User

  action_fallback AccordWeb.FallbackController

  def create(conn, params) do
    with {:ok, %User{} = user} <- Accord.Account.create_user(params) do
      conn
      |> put_status(:created)
      |> render("create.json", user: user)
    end
  end
end
