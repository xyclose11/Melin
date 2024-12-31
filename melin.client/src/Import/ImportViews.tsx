import {
    Tabs,
    TabsList,
    TabsContent,
    TabsTrigger,
} from "@/components/ui/tabs.tsx";
import { FormattedView } from "@/Import/FormattedView.tsx";
import { RawView } from "@/Import/RawView.tsx";
import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
    CardDescription,
} from "@/components/ui/card.tsx";
import { Button } from "@/components/ui/button.tsx";
import { ReferenceType } from "@/Import/ImportFile.tsx";
import { FormProvider, useFieldArray, useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useRef } from "react";
import { DevTool } from "@hookform/devtools";

const rawFormSchema = z.object({
    rawDataArray: z.array(z.string()).optional(),
});
export function ImportViews({
    rawData,
    formattedData,
}: {
    rawData: string[];
    formattedData: ReferenceType[];
}) {
    const methods = useForm<z.infer<typeof rawFormSchema>>({
        resolver: zodResolver(rawFormSchema),
        defaultValues: { rawDataArray: [] },
    });

    const { fields, append } = useFieldArray({
        control: methods.control,
        name: "rawDataArray",
    });
    const onSubmit = (values: any) => {
        console.log(values);
    };

    useEffect(() => {
        console.log(rawData);
        if (rawData.length > 0) {
            rawData.forEach((data) => append(data));
        }
    }, [rawData]);

    return (
        <div>
            <FormProvider {...methods}>
                <form onSubmit={methods.handleSubmit(onSubmit)}>
                    <Card>
                        <CardHeader>
                            <CardTitle>Parsed Data</CardTitle>
                            <CardDescription></CardDescription>
                        </CardHeader>
                        <CardContent>
                            <Tabs
                                defaultValue="formatted"
                                className="w-[400px]"
                            >
                                <TabsList>
                                    <TabsTrigger value="formatted">
                                        Formatted
                                    </TabsTrigger>
                                    <TabsTrigger value="raw">Raw</TabsTrigger>
                                </TabsList>
                                <TabsContent value="formatted">
                                    {formattedData.map((f) => (
                                        <FormattedView key={f.id} data={f} />
                                    ))}
                                </TabsContent>
                                <TabsContent value="raw">
                                    {/*{rawData.map((r) => (*/}
                                    {/*    <RawView rawData={r} />*/}
                                    {/*))}*/}
                                    {fields.map((item, index) => {
                                        console.log(item);
                                        return (
                                            <li key={item.id}>
                                                <RawView
                                                    name={`rawDataArray.${index}`}
                                                />
                                            </li>
                                        );
                                    })}
                                </TabsContent>
                            </Tabs>
                        </CardContent>
                        <CardFooter>
                            <Button type="reset" variant="destructive">
                                Cancel
                            </Button>
                            <Button type="submit" variant="secondary">
                                Import
                            </Button>
                        </CardFooter>
                    </Card>
                </form>
            </FormProvider>
            <DevTool control={methods.control} />
        </div>
    );
}
