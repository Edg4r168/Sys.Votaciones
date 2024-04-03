import { useState } from "react";
import { Badge } from "src/Components/Badges/Badge";
import { ArroyDownIcon, CrossIcon } from "src/Components/Icons";
import { ButtonShowMore, RoundButtonAdd } from "src/Components/Layout/Buttons";
import { ModalPortal } from "src/Components/Modal/Modal";
import { Notification } from "src/Components/Notification/Notification";
import { SearchForm } from "src/Components/SearchForm/SearchForm";
import { Card } from "src/Components/Table/Card";
import { Table } from "src/Components/Table/Table";
import { useTable } from "src/hooks/useTable";
import { FormSave } from "./FormSave";
import { CardHeader } from "src/Components/Table/CardHeader";
import { Title } from "src/Components/Table/Title";
import { CardFooter } from "src/Components/Table/CardFooter";
import { Thead } from "src/Components/Table/Thead";
import { Tbody } from "src/Components/Table/Tbody";
import { ListOfContests } from "./ListOfContests";

export function TContest() {
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

          <SearchForm searchBy={"studentCode"} />

          <RoundButtonAdd onClick={() => setShowModal(true)}>
            <CrossIcon />
          </RoundButtonAdd>
        </CardHeader>

        <Table className="text-center">
          <Thead>
            <th>Id</th>
            <th>Nombre</th>
            <th>Descripción</th>
            <th>stado</th>
            <th>Tipo de consurso</th>
            <th>Acciones</th>
          </Thead>

          <Tbody entries={entries}>
            <ListOfContests />
          </Tbody>
        </Table>

        <CardFooter>
          <ButtonShowMore
            disabled={loading}
            onClick={getPaginated}
            style={{ display: total === entries.length ? "none" : "block" }}
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
