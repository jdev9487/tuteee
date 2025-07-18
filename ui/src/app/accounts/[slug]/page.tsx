import Link from "next/link"
import { accountResponse } from "../../types/GuardianResponse"
import { List, ListItem, ListItemText, Typography } from "@mui/material"

export default async function Page({
  params,
}: {
  params: Promise<{ slug: number }>
}) {
  const { slug } = await params
  const data = await fetch(`${process.env.NEXT_PUBLIC_API}/accounts/${slug}`, { cache: 'no-store' })
  const account = (await data.json()) as accountResponse

  function getChildren() {
    if (account.tutees.length !== 0) {
      return (
        <div>
          <List>
            {account.tutees.map((t, i) => 
              <ListItem key={i} disablePadding>
                <Link href={`/tutees/${t.tuteeId}`}><ListItemText primary={`${t.firstName} ${t.lastName}`} /></Link>
              </ListItem>
            )}
          </List>
        </div>
      )
    }
  }

  return (
    <div>
      <Typography variant="h1" align="center">
        {account.holderFirstName} {account.holderLastName} 📂
      </Typography>
      <Typography variant="h3">Tutees</Typography>
      {getChildren()}
    </div>
  )
}