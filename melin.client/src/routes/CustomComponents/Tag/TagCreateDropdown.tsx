import { Input } from "@/components/ui/input.tsx";
import { FormControl, FormField, FormItem } from "@/components/ui/form.tsx";
import { useFormContext } from "react-hook-form";
import { z } from "zod";

export function TagCreateDropdown() {
    const { control } = useFormContext();

    return (
        <div>
            <FormField
                control={control}
                render={({ field }) => (
                    <FormItem>
                        <FormControl>
                            <Input type="text" placeholder="Tags" {...field} />
                        </FormControl>
                    </FormItem>
                )}
                name={"tagInput"}
            ></FormField>
        </div>
    );
}
