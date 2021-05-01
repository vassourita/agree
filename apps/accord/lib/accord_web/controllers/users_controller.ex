defmodule AccordWeb.UsersController do
  use AccordWeb, :controller
  alias Accord.User

  action_fallback AccordWeb.FallbackController

  def create(conn, params) do
    with {:ok, %User{} = user} <- Accord.create_user(params) do
      conn
      |> put_status(:created)
      |> render("create.json", user: user)
    end
  end
end
