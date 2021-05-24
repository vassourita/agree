defmodule AccordWeb.CategoryController do
  use AccordWeb, :controller

  alias Accord.Servers
  alias Accord.Servers.Category

  action_fallback AccordWeb.FallbackController

  def index(conn, _params) do
    category = Servers.list_category()
    render(conn, "index.json", category: category)
  end

  def create(conn, %{"category" => category_params}) do
    with {:ok, %Category{} = category} <- Servers.create_category(category_params) do
      conn
      |> put_status(:created)
      |> put_resp_header("location", Routes.category_path(conn, :show, category))
      |> render("show.json", category: category)
    end
  end

  def show(conn, %{"id" => id}) do
    category = Servers.get_category!(id)
    render(conn, "show.json", category: category)
  end

  def update(conn, %{"id" => id, "category" => category_params}) do
    category = Servers.get_category!(id)

    with {:ok, %Category{} = category} <- Servers.update_category(category, category_params) do
      render(conn, "show.json", category: category)
    end
  end

  def delete(conn, %{"id" => id}) do
    category = Servers.get_category!(id)

    with {:ok, %Category{}} <- Servers.delete_category(category) do
      send_resp(conn, :no_content, "")
    end
  end
end
