defmodule Accord.Repo.Migrations.CreateChannel do
  use Ecto.Migration

  def change do
    create table(:channel, primary_key: false) do
      add :id, :binary_id, primary_key: true
      add :name, :string
      add :category_id, references(:category, on_delete: :delete_all, type: :binary_id)

      timestamps()
    end

    create index(:channel, [:category_id])
  end
end
