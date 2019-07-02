using System;
using System.Collections.Concurrent;
using System.Threading;

using Lingvistics;
using Lingvistics.Client;

namespace TonalityMarkingAndDigestInProc.web.demo
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class ConcurrentFactory
	{
        /// <summary>
        /// 
        /// </summary>
        public struct InputParams
        {
            #region [.ONLY 1, NO thread-safety! because fusing Oleg made a shit, fusk his mother and him!.]
            //public int InstanceCount { get; set; } 
            #endregion
            public int MaxEntityLength { get; set; }
            public bool UseGeoNamesDictionary { get; set; }
            public bool UseCoreferenceResolution { get; set; }
        }
		
        private readonly ConcurrentStack< LingvisticsWorkInProcessor > _Stack;
        private readonly Semaphore _Semaphore;

        public ConcurrentFactory( InputParams ip )
		{            
            if ( ip.MaxEntityLength <= 0 ) throw (new ArgumentNullException("MaxEntityLength"));

            #region [.ONLY 1, NO thread-safety! because fusing Oleg made a shit, fusk his mother and him!.]
            /*
            if ( ip.InstanceCount   <= 0 ) throw (new ArgumentException("InstanceCount"));

            _Semaphore = new Semaphore( ip.InstanceCount, ip.InstanceCount );            
            _Stack = new ConcurrentStack<LingvisticsWorkInProcessor>();
            for ( int i = 0; i < ip.InstanceCount; i++ )
            {
                _Stack.Push( new LingvisticsWorkInProcessor( ip.UseCoreferenceResolution, ip.UseGeoNamesDictionary, ip.MaxEntityLength ) );
            }*/
            #endregion

            const int INSTANCE_COUNT = 1;
            _Semaphore = new Semaphore( INSTANCE_COUNT, INSTANCE_COUNT );
            _Stack = new ConcurrentStack< LingvisticsWorkInProcessor >();
            for ( int i = 0; i < INSTANCE_COUNT; i++ )
            {
                _Stack.Push( new LingvisticsWorkInProcessor( ip.UseCoreferenceResolution, ip.UseGeoNamesDictionary, ip.MaxEntityLength ) );
            }

            #region commented
            /*
            Parallel.For( 0, ip.InstanceCount, _ =>            
                _Stack.Push( new LingvisticsWorkInProcessor( ip.UseCoreferenceResolution, ip.UseGeoNamesDictionary, ip.MaxEntityLength ) )
            );
            */
            #endregion
        }

        public LingvisticsResult ProcessText( LingvisticsTextInput input )
		{
			_Semaphore.WaitOne();
			var worker = default(LingvisticsWorkInProcessor);
			var result = default(LingvisticsResult);
			try
			{
                worker = Pop( _Stack );
                result = worker.ProcessText( input );
			}
			finally
			{
                if ( worker != null )
				{
                    _Stack.Push( worker );
				}
				_Semaphore.Release();
			}
			return (result);
		}

        private static T Pop< T >( ConcurrentStack< T > stack )
        {
            T t;
            if ( stack.TryPop( out t ) )
            {
                return (t);
            }
            return (default(T));
        }
	}
}
