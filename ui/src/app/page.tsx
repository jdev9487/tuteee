import Link from "next/link"
import Typography from '@mui/material/Typography';
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { accountResponse } from "./types/GuardianResponse";

export const dynamic = "force-dynamic"

export default async function Page() {
  const data = await fetch(`${process.env.NEXT_PUBLIC_API}/accounts`, { cache: 'no-store' })
  const guardians = (await data.json()) as accountResponse[]
  return (
    <div>
      <Typography variant="h1" align="center">Accounts</Typography>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Holder</TableCell>
              <TableCell>Tutees</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {guardians.map((g, i) => 
              <TableRow key={i}>
                <TableCell>
                  <Typography><Link key={i} href={`/accounts/${g.accountId}`}>{`${g.holderFirstName} ${g.holderLastName}`}</Link></Typography>
                </TableCell>
                <TableCell>
                  {g.tutees.map((t, j) => <Typography key={j}><Link href={`/tutees/${t.tuteeId}`}>{`${t.firstName} ${t.lastName}`}</Link></Typography>)}
                  </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  )
}