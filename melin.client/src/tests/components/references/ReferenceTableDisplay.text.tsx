import { test } from "vitest";
import { render, screen } from "@testing-library/react";
import { Library } from "@/routes/Library.tsx";
import userEvent from "@testing-library/user-event";

test("loads and displays references", async () => {
    render(<Library />);

    await userEvent.click(screen.getByText("Date Published"));
});
