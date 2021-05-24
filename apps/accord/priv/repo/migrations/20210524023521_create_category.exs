defmodule Accord.Repo.Migrations.CreateCategory do
  use Ecto.Migration

  def change do
    create table(:category, primary_key: false) do
      add :id, :binary_id, primary_key: true
      add :name, :string
      add :server_id, references(:server, on_delete: :delete_all, type: :binary_id)

      timestamps()
    end

    create index(:category, [:server_id])
  end
end
