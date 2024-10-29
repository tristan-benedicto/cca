import UsersTable from "@/app/users/table";
import { DefaultPagination, prefetchPaginatedQuery } from "@/hooks/query";
import { HydrationBoundary, dehydrate } from "@tanstack/react-query";
import StatsTable from "./table";

const StatsPage = async () => {
  const queryClient = await prefetchPaginatedQuery("/Stat", DefaultPagination);

  return (
    <main>
      <HydrationBoundary state={dehydrate(queryClient)}>
        <StatsTable />
      </HydrationBoundary>
    </main>
  );
};

export default StatsPage;
