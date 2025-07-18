import Link from "next/link"
import { guardianResponse } from "../../types/GuardianResponse"
import { List, ListItem, ListItemText, Typography } from "@mui/material"

export default async function Page({
  params,
}: {
  params: Promise<{ slug: number }>
}) {
  const { slug } = await params
  const data = await fetch(`${process.env.NEXT_PUBLIC_API}/guardians/${slug}`, { cache: 'no-store' })
  const guardian = (await data.json()) as guardianResponse

  function getChildren() {
    if (guardian.tutees.length !== 0) {
      return (
        <div>
          <List>
            {guardian.tutees.map((t, i) => 
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
        {guardian.firstName} {guardian.lastName} 🧑
      </Typography>
      <Typography variant="h3">
        Children
      </Typography>
      {getChildren()}
    </div>
  )
}