import "./index.css";
import { Table } from "src/Components/Table/Table";
import { Notification } from "src/Components/Notification/Notification";
import { useState } from "react";
import { ModalPortal } from "src/Components/Modal/Modal";
import { Card } from "src/Components/Table/Card";
import { SearchForm } from "src/Components/SearchForm/SearchForm";
import { FormSave } from "./FormSave";
import { ListOfCategories } from "./ListOfCategories";
import { ArroyDownIcon, CrossIcon } from "src/Components/Icons";
import { ButtonShowMore, RoundButtonAdd } from "src/Components/Layout/Buttons";
import { Badge } from "src/Components/Badges/Badge";
import { useTable } from "src/hooks/useTable";
import { Title } from "src/Components/Table/Title";
import { CardHeader } from "src/Components/Table/CardHeader";
import { CardFooter } from "src/Components/Table/CardFooter";
import { Thead } from "src/Components/Table/Thead";
import { Tbody } from "src/Components/Table/Tbody";

export function TCategory() {
  const [showModal, setShowModal] = useState(false);
  const { loading, messages, total, entries, getPaginated } = useTable();

  const handleOnClose = () => {
    setShowModal(false);
  };

  return (
    <>
      {loading && <style>{"body { cursor: progress; }"}</style>}

      {messages.error && (
        <Notification type="error" duration={3000}>
          {messages.error}
        </Notification>
      )}

      {messages.success && (
        <Notification type="success" duration={2000}>
          {messages.success}
        </Notification>
      )}

      <Card>
        <CardHeader>
          <Title>
            Total
            <Badge>{total}</Badge>
          </Title>

          <SearchForm searchBy="name" />

          <RoundButtonAdd onClick={() => setShowModal(true)}>
            <CrossIcon />
          </RoundButtonAdd>
        </CardHeader>

        <Table className="text-center">
          <Thead>
            <th>Id</th>
            <th>Nombre</th>
            <th>Acciones</th>
          </Thead>

          <Tbody entries={entries}>
            <ListOfCategories />
          </Tbody>
        </Table>

        <CardFooter>
          <ButtonShowMore
            disabled={loading}
            onClick={getPaginated}
            style={{ display: entries.length >= total ? "none" : "block" }}
          >
            <ArroyDownIcon />
          </ButtonShowMore>
        </CardFooter>
      </Card>

      <ModalPortal onClose={handleOnClose} openModal={showModal}>
        <FormSave onCancel={handleOnClose} onSubmit={handleOnClose}></FormSave>
      </ModalPortal>
    </>
  );
}
