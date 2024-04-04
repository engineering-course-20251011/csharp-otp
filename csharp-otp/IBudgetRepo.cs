using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public interface IBudgetRepo
    {
        List<Budget> GetAll();
    }
}
