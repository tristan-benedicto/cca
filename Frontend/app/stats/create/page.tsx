"use client";

import Button from "@/components/Button";
import TextInput from "@/components/TextInput";
import { Form } from "@/components/Form";
import { useApiMutation } from "@/hooks/query";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { toast } from "react-toastify";

interface CreateStatRequest {
  hour: Date;
  callCount: number;
  topUser: string;
}

interface CreateStatResponse {
  id: string;
}

const CreateStatPage = () => {
  const router = useRouter();

  const [topUser, setTopUser] = useState("");
  const [callCount, setCallCount] = useState(0);
  const [hour, setHour] = useState(new Date());

  const { mutateAsync, isIdle } = useApiMutation<CreateStatResponse, CreateStatRequest>("/Stats", "post", {
    onSuccess() {
      toast.success("Stat created successfully");
      router.replace("/stats");
    },
    onError(error) {
      toast.error(error.join(","));
    },
  });

  const onSubmit = () => {
    if (isIdle) {
      mutateAsync({ topUser, hour, callCount });
    }
  };

  return (
    <Form>
      <Form.Header
        title="Stats"
        description="This information will be displayed publicly so be careful what you share."
      />
      <Form.Section>
        <TextInput
          id="topUser"
          label="Top User"
          placeholder="input top user"
          type="text"
          name="topUser"
          autoComplete="off"
          value={topUser}
          onChange={(e) => setTopUser(e.target.value)}
        />
         <TextInput
          id="callCount"
          label="Call Count"
          placeholder="input Call Count"
          type="text"
          name="callCount"
          autoComplete="off"
          value={topUser}
          onChange={(e) => setCallCount(parseInt(e.target.value))}
        />
         <TextInput
          id="hour"
          label="Hour"
          placeholder="input Hour"
          type="text"
          name="hour"
          autoComplete="off"
          value={hour.toString()}
          onChange={(e) => setHour(new Date(e.target.value))}
        />
      </Form.Section>
      <Form.Section>
        <Button
          onClick={onSubmit}
          type="button"
          className={isIdle === false ? "cursor-not-allowed opacity-50" : ""}
        >
          Create Stats
        </Button>
      </Form.Section>
    </Form>
  );
};

export default CreateStatPage;
