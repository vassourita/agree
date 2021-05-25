defmodule Accord.Repo.Migrations.CreateServer do
  use Ecto.Migration

  def change do
    create table(:server, primary_key: false) do
      add :id, :binary_id, primary_key: true
      add :name, :string, null: false
      add :description, :string
      add :privacy, :integer, null: false

      timestamps()
    end
  end
end
