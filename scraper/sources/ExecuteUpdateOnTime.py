from datetime import datetime
from datetime import timedelta
from time import sleep
from os import system


if __name__ == "__main__":
    current_date = datetime.now()
    target_date = current_date + timedelta(days=1)
    target_date = target_date.replace(hour=2, minute=0, second=0, microsecond=0)
    while True:
        current_date = datetime.now()
        current_date = current_date.replace(second=0, microsecond=0)
        if current_date == target_date:
            print(f"Updating the database at {target_date}")
            system("UpdateProducts.exe")

            target_date = target_date + timedelta(days=2)
            print(f"Updating finished, scheduling next update at {target_date}")
        else:
            print(f"Waiting for {target_date}...")
        sleep(60)
