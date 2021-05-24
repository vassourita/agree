defmodule Accord.Repo.Migrations.CreateServer do
  use Ecto.Migration

  def change do
    create table(:server, primary_key: false) do
      add :id, :binary_id, primary_key: true
      add :name, :string
      add :description, :string
      add :privacy, :integer

      timestamps()
    end

  end
end
